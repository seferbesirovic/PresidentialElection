import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { GetStateWithResultsDto } from '../models/GetStateWithResultsDto';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  states: BehaviorSubject<GetStateWithResultsDto[]>;

  constructor() {
    this.states = new BehaviorSubject([]);
  }

  changeStates(states) {
    this.states.next(states);
  }
}
