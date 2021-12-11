import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ElectionResultsComponent } from './components/election-results/election-results.component';


const routes: Routes = [
  { path: 'electionResults', component: ElectionResultsComponent },
  { path: '', redirectTo: 'electionResults', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
