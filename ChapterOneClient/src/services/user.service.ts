import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Name : [''],
    Surname : [''],
    BirthDate : [''],
    Username : ['', Validators.required],
    Email : ['', Validators.email],
    PasswordHash : ['', [Validators.required, Validators.minLength(8)]],
    Subscription : ['']

  })

  login(formData: any){
    return this.http.post(environment.baseURI + 'UserAuth/login', formData);
  }
  getDecodedAccessToken(token: string): any {
    try{
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }
  logout() {
    localStorage.removeItem('accessToken');
    this.router.navigateByUrl('/login');
  }

  registerUser(){
    var body = {
      Name : this.formModel.value.Name,
      Surname : this.formModel.value.Surname,
      BirthDate : this.formModel.value.BirthDate,
      Username : this.formModel.value.Username,
      Email : this.formModel.value.Email,
      PasswordHash : this.formModel.value.PasswordHash,
      Subscription : this.formModel.value.Subscription
    };
    return this.http.post(environment.baseURI + 'UserAuth/registration', body)
  }

  registerAdmin(){
    var body = {
      Name : this.formModel.value.Name,
      Surname : this.formModel.value.Surname,
      BirthDate : this.formModel.value.BirthDate,
      Username : this.formModel.value.Username,
      Email : this.formModel.value.Email,
      PasswordHash : this.formModel.value.PasswordHash,
      Subscription : this.formModel.value.Subscription
    };
    return this.http.post(environment.baseURI + 'UserAuth/registration-admin', body)
  }

  getSubscriptionsList() {
    return this.http.get(environment.baseURI + 'Subscription')
  }
  
}
