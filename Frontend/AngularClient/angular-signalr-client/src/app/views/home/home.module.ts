import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { homeRoutes } from './home.routing';
import { presentanialComponents } from '../../components/index';


@NgModule({
    declarations: [...presentanialComponents],
    imports: [
      CommonModule,
      ReactiveFormsModule,
      RouterModule.forChild(homeRoutes)
    ]
  })
  export class HomeModule {}