import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dish } from '../_models/dish';
import { DishFilter } from '../_models/dish-filter';

@Injectable({
  providedIn: 'root'
})
export class DishService {

baseURL = 'http://localhost:5000/api/dish';

constructor(private http: HttpClient) { }

getAllDishes(): Observable<Dish[]> {
  return this.http.get<Dish[]>(this.baseURL);
}

SearchDishes(filter: DishFilter): Observable<Dish[]> {
  return this.http.post<Dish[]>(this.baseURL + '/SearchDishes', filter);
}

}
