import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import jwt_decode from 'jwt-decode';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) { }

  public formModel = this.fb.group({
    Name : [''],
    Surname : [''],
    BirthDate : [''],
    Username : ['', Validators.required],
    Email : ['', Validators.email],
    PasswordHash : ['', Validators.compose([Validators.required, Validators.minLength(8), Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&].{8,}$")])],
    Subscription : ['']

  })

  public formUpdateModel = this.fb.group({
    Name : [''],
    MiddleName : [''],
    Surname : [''],
    BirthDate : [''],
    PhoneNumber : [''],
    Address : [''],
    Username : ['', Validators.required],
    Email : ['', Validators.email]
  })

  public formUpdateByAdminModel = this.fb.group({
    Name : [''],
    Surname : [''],
    BirthDate : [''],
    Username : ['', Validators.required],
    Email : ['', Validators.email]
  })

  login(formData: any){
    return this.http.post(environment.baseURI + 'UserAuth/login', formData);
  }

  logout() {
    localStorage.removeItem('accessToken');
    this.router.navigateByUrl('/login');
  }

  getDecodedAccessToken(token: string): any {
    try{
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
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

  getUserById(id: string) {
    return this.http.get(environment.baseURI + 'User/' + id);
  }

  getCurrentUser() {
    return this.http.get(environment.baseURI + 'User/current-user', {headers: this.tokenHeader});
  }

  getAllUsers() {
    return this.http.get(environment.baseURI + 'User');
  }

  updateProfile(body: any) {
    body = {
      Name : this.formUpdateModel.value.Name,
      MiddleName : this.formUpdateModel.value.MiddleName,
      Surname : this.formUpdateModel.value.Surname,
      BirthDate : this.formUpdateModel.value.BirthDate,
      PhoneNumber : this.formUpdateModel.value.PhoneNumber,
      Address : this.formUpdateModel.value.Address,
      Username : this.formUpdateModel.value.Username,
      Email : this.formUpdateModel.value.Email
    };
    return this.http.put(environment.baseURI + 'User', body)
  }

  updateUserByAdmin(body: any) {
    body = {
      Name : this.formUpdateByAdminModel.value.Name,
      Surname : this.formUpdateByAdminModel.value.Surname,
      BirthDate : this.formUpdateByAdminModel.value.BirthDate,
      Username : this.formUpdateByAdminModel.value.Username,
      Email : this.formUpdateByAdminModel.value.Email
    };
    return this.http.put(environment.baseURI + 'User', body)
  }

  deleteUser(id: string) {
    return this.http.delete(environment.baseURI + 'User/' + id, {headers: this.tokenHeader})
  }

  getImagePath(relativePath: string) : Observable<Blob> {
    let result = relativePath.substring(16, relativePath.length);
    return this.http.get(environment.baseURI + 'Image?fileName=' + result, {responseType: 'blob'});
  }

  uploadImage(userId: string, file: File): Observable<any>  {
    const formData = new FormData();
    formData.append("profileImage", file);
    return this.http.post(environment.baseURI + 'Image/' + userId + '/upload-image', 
    formData, {
      responseType: 'text'
    });
  }
}
