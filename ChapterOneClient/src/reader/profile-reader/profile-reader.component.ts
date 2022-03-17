import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-profile-reader',
  templateUrl: './profile-reader.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class ProfileReaderComponent implements OnInit {

  userDetails: any;
  minDate: Date;

  constructor(private router: Router, public userService: UserService, private toastr: ToastrService) { 
    const currentYear = new Date().getFullYear();
    this.minDate = new Date(currentYear - 14, 11, 31);
  }

  ngOnInit(): void {
    document.body.style.overflow = "hidden";
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        console.log(this.userDetails);
      },
      err =>{
        console.log(err);
      }
    );
  }

  onSubmit()
  {
    this.userService.updateProfile(this.userDetails).subscribe( 
      result => {
        this.toastr.success('Profile updated successfully');
        window.location.reload();
      }, 
      error  => {
        console.log(error);
      }
    )

  }

}
