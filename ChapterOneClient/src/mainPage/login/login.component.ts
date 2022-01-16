import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['../mainStyles.component.scss']
})
export class LoginComponent implements OnInit {

  hide = true;
  formModel = {
    Username : '',
    PasswordHash : ''
  }
  constructor(private service: UserService, private router: Router, private toastr: ToastrService) {
  }

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    this.service.login(form.value).subscribe(
      (res:any) => {
        localStorage.setItem('accessToken', res.accessToken);
        if(res != undefined)
        {
          try{
            let tokenInfo = this.service.getDecodedAccessToken(res.accessToken);
            console.log(tokenInfo);
            if(tokenInfo.role === "Admin")
            {
              this.toastr.success("Successfully logged in as " + tokenInfo.unique_name);
            } else if(tokenInfo.role === "User") {
              this.toastr.success("Successfully logged in as " + tokenInfo.unique_name);
            }
          }
          catch(error) {
              console.error("Error getting JSON")
          }
        }
      },
      (err: HttpErrorResponse) => {
        this.toastr.error(`${err.error.title}`);
        for(let i in err.error.errors) {
          this.toastr.error(`${err.error.errors[i]}`);
        }
        console.log(err);
      }
    )
}

}
