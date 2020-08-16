import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { User } from '../_models/User';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  nome: User;
  constructor(public authService: AuthService,
    public toastr: ToastrService,
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

  entrar() {
    this.router.navigate(['/user/login']);
  }

  showMenu() {
    return this.router.url !== '/user/login';
  }

  userName() {
    return sessionStorage.getItem('username');
  }

}
