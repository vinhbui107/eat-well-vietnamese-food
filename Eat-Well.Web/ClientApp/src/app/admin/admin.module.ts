import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { Routes, RouterModule } from '@angular/router';

const  routes:  Routes  = [
  {
  path:  'admin',
  component:  AdminComponent,
  children: [



  ]
  }
  ];

@NgModule({
  declarations: [AdminComponent],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminModule { }
