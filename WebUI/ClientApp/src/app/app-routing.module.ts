import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RequestComponent } from './request/request.component';
import { ReviewComponent } from './review/review.component';


const routes: Routes = [
  { path: '', redirectTo: '/requests', pathMatch: 'full' },
  { path: 'requests', component: RequestComponent },
  { path: 'reviews', component: ReviewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
