import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-sidebar-reader',
  templateUrl: './sidebar-reader.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class SidebarReaderComponent implements OnInit {
  userDetails: any;
  freeSubs: any;
  mediumSubs: any;
  highSubs: any;
  url = '';

  constructor(private router: Router, private toastr: ToastrService, private userService: UserService) {
  }
  OnSelectFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); 

      reader.onload = (event) => { 
        this.url = <string>reader.result;
      }
    }
  }

  ngOnInit(): void {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        this.addImageToSidebar(this.userDetails.subscription.name);
      },
      err =>{
        console.log(err);
      }
    );
  }

  addImageToSidebar(name: any) {
    if(name === "Free") {
      this.freeSubs = "Free";
    } else if(name === "Medium") {
      this.mediumSubs = "Medium";
    } else {
      this.highSubs = "High";
    }
  } 

}
