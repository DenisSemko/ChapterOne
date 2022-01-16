import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['../mainStyles.component.scss']
})
export class RegistrationComponent implements OnInit {

  hide = true;
  minDate: Date;
  subscriptionsList: any;

  constructor(public service: UserService, private toastr: ToastrService) { 
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 10, 11, 31);
  }

  ngOnInit(): void {

    this.service.formModel.reset();

    this.service.getSubscriptionsList().subscribe(
      res => {
        this.subscriptionsList = res
      },
      err =>{
        console.log(err);
      }
    );
  }

  onSubmit()
  {
    this.service.registerUser().subscribe(
      (res:any) => {
          this.service.formModel.reset();
          this.toastr.success('New user has been successfully created!', 'Registration successful.');
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
