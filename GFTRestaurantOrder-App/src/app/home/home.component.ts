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
  
  textOrder: string;
  textOutput: string;
  dishesHistorys: DishHistory[];
  filter: DishFilter;

  constructor(
      private dishService: DishService      
    ) {
        this.dishesHistorys = [];        
      }
  
  sendOrder() {    
    this.filter = new DishFilter();
    this.textOrder = this.textOrder.toLowerCase();

    let arrayOrder = this.textOrder.split(',');
    
    if (arrayOrder && arrayOrder.length > 1) {
        this.filter.timeOfDay = arrayOrder[0];

        for (let i = 1; i < arrayOrder.length; i++) {          
          this.filter.types.push(parseInt(arrayOrder[i]));
        }

        this.searchDishes(this.filter);
    }
    
  }

  searchDishes(filter: DishFilter) {
    
    this.dishService.SearchDishes(filter).subscribe(
      (_dishes: Dish[]) => {
        this.textOutput = '';        
        let oldDish = new Dish();
        let cont = 1;

        for (let i = 0; i < _dishes.length; i++) {
          const dish = _dishes[i];

          if (i == 0)
            { this.textOutput = dish.description; }
          else if (dish.description == oldDish.description)
            { cont++; }
          else if (cont > 1)
            {              
              this.textOutput += ` (x${cont})`;
              cont = 1;
              this.textOutput += ',' + dish.description;
            }
          else
            { this.textOutput += ',' + dish.description; }

          oldDish = dish;
        }        
      }, 
      error => {
        console.error(error);
        this.textOutput = `${error.status} ${error.error}`;
      }
    ).add(
      () => {
        this.addHistory();
      }
    );
  }
  
  addHistory() {
    let history = new DishHistory();
    history.input = this.textOrder;
    history.output = this.textOutput;

    this.dishesHistorys.unshift(history);
  }

  ngOnInit() {
    
  }

}
