import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})

export class NavComponent implements OnInit {

  model: any = { };

  constructor(
    public accountService: AccountService, 
    private router: Router) { }

  ngOnInit(): void { }

  login(){
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      // error is handled in interceptor!
    });
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
