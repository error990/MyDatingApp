import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';

// TO KEEP THE app.module.ts AS TIDY AS POSSIBLE

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
  ],
  // EXPORTS TO USE FUNCTIONALITY IN APP.MODULE --> IS SHARED MODULE IS IMPORTED THERE
  exports: [
    BsDropdownModule,
    ToastrModule,
  ]
})
export class SharedModule { }
