import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService,
    private toastr: ToastrService,
    public router: Router) { }

  ngOnInit() {
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.toastr.show("LogOut !!");
    this.router.navigate(['/user/login']);
  }

}
