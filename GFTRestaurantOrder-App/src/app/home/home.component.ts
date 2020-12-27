import { Component, OnInit } from '@angular/core';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { Dish } from '../_models/dish';
import { DishFilter } from '../_models/dish-filter';
import { DishHistory } from '../_models/dish-history';
import { DishService } from '../_services/dish.service';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  // fields
  textOrder: string;
  textOutput: string;
  dishesHistorys: DishHistory[];
  filter: DishFilter;

  // Constructor
  constructor(
      private dishService: DishService
    ) {
        this.dishesHistorys = [];
      }

  // Begin Methods
  sendOrder() {
    this.filter = new DishFilter();
    this.textOrder = this.textOrder.toLowerCase();

    const arrayOrder = this.textOrder.split(',');

    if (arrayOrder && arrayOrder.length > 1) {
        this.filter.timeOfDay = arrayOrder[0];

        for (let i = 1; i < arrayOrder.length; i++) {
          this.filter.types.push(parseInt(arrayOrder[i], 10));
        }

        this.searchDishes(this.filter);
    }

  }

  // Action's button 'Send Order'
  searchDishes(filter: DishFilter) {

    this.dishService.SearchDishes(filter).subscribe(
      (_dishes: Dish[]) => {
        this.textOutput = '';
        let validRepetition = true;
        let oldDish = new Dish();
        let cont = 1;

        // loop for dishes
        for (let i = 0; i < _dishes.length; i++) {
          const dish = _dishes[i];

          // organize output text
          if (i == 0)
            { this.textOutput = dish.description; }
          else if (dish.description == oldDish.description) {
            cont++;
            if (i >= _dishes.length - 1)
            {
              this.textOutput += ` (x${cont})`;
              validRepetition = this.validateRepetition(oldDish, validRepetition);
            }
          }
          else if (cont > 1)
            {
              this.textOutput += ` (x${cont})`;
              validRepetition = this.validateRepetition(oldDish, validRepetition);
              this.textOutput += ', ' + dish.description;
              cont = 1;
            }
          else
            { this.textOutput += ', ' + dish.description; }

          // set new value in oldDish var
          oldDish = dish;
        }

        if (validRepetition === false) {
          this.textOutput += ' INVALID ORDER (Only Cofee and potatoes can be repeated)';
        }
      },
      error => {
        this.textOutput = `${error.status} ${error.error}`;
      }
    ).add(
      () => {
        this.addHistory();
      }
    );
  }

  // Add a row in History Grid
  addHistory() {
    const history = new DishHistory();
    history.input = this.textOrder;
    history.output = this.textOutput;

    this.dishesHistorys.unshift(history);
  }

  validateRepetition(oldDish: Dish, validRepetition: bool): boolean {
    if (!validRepetition){
      return false;
    }

    if (oldDish.description.toLowerCase() == 'coffee'
          || oldDish.description.toLowerCase() == 'potato') {
      return true;
    }
    return false;
  }

  // End Methods

  // Inicialize
  ngOnInit() {

  }

}
