import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { GetStateWithResultsDto } from 'src/app/models/GetStateWithResultsDto';
import { ResultsService } from 'src/app/services/results.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-election-results',
  templateUrl: './election-results.component.html',
  styleUrls: ['./election-results.component.css']
})
export class ElectionResultsComponent implements OnInit {

  private readonly onDestroy = new Subject<void>();
  step = 0;
  states: GetStateWithResultsDto[];
  displayedColumns: string[] = ['name', 'votes', 'percentage', 'error'];

  constructor(private resultService: ResultsService, private sharedService: SharedService) {

  }

  setStep(index: number) {
    this.step = index;
  }

  ngOnInit() {
    this.sharedService.states.subscribe(states => this.states = states);
    this.getStatesWithResults();
  }

  getStatesWithResults() {
    this.resultService.getAllResults().pipe(takeUntil(this.onDestroy))
      .subscribe(
        (data: GetStateWithResultsDto[])  => {
          this.states = data;
        },
        error => {
          console.log(error);
        }
      );
  }

  ngOnDestroy() {
    this.onDestroy.next();
  }

}
