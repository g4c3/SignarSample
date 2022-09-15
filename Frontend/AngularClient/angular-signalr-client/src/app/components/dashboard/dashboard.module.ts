import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { presentanialComponents } from '../';
import { RouterModule } from '@angular/router';
import { homeRoutes } from './dashboard.routing';

@NgModule({
  declarations: [...presentanialComponents],
  imports: [
    CommonModule,
    RouterModule.forChild(homeRoutes)
  ]
})
export class DashboardModule { }
