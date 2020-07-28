import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.validation();
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(25)]],      
      email  : ['', [Validators.required, Validators.email]],            
      userName: ['', Validators.required],      
      passwords : this.fb.group({
        password : ['', [Validators.required, Validators.minLength(4)]],           
        confirmPassword: ['', Validators.required]
      }, {validator: this.compararSenhas})
      
    });
  }

  compararSenhas(fb: FormGroup) {
    const confirmarSenhaCtrl = fb.get('confirmPassword');
    if (confirmarSenhaCtrl.errors == null || 'mismatch' in confirmarSenhaCtrl.errors) {
      if (fb.get('password').value !== confirmarSenhaCtrl.value) {
        fb.get('confirmPassword').setErrors({mismatch: true});
      }else{
        fb.get('confirmPassword').setErrors(null);
      }
    }
  }

  cadastrarUsuario() {
    console.log('cadastrar user');
  }



}
