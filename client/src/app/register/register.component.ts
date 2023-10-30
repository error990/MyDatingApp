import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}
  
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: err => {
        console.log(err);
        for(let e in err.error.errors) {
          this.toastr.error(e);
        }
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
