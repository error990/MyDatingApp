import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FileUploadModule } from 'ng2-file-upload';

// TO KEEP THE app.module.ts AS TIDY AS POSSIBLE

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({
      type: 'pacman'
    }),
    FileUploadModule
  ],
  // EXPORTS TO USE FUNCTIONALITY IN APP.MODULE --> IS SHARED MODULE IS IMPORTED THERE
  exports: [
    BsDropdownModule,
    TabsModule,
    ToastrModule,
    NgxSpinnerModule,
    FileUploadModule
  ]
})
export class SharedModule { }
