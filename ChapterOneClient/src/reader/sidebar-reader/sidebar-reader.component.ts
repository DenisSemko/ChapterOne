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
  imageToShow: any;

  constructor(private router: Router, private toastr: ToastrService, private userService: UserService) {
  }

  ngOnInit(): void {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        this.addIconToSidebar(this.userDetails.subscription.name);
        this.setImage(this.userDetails.profileImage);
      },
      err =>{
        console.log(err);
      }
    );
  }

  private addIconToSidebar(name: any) {
    if(name === "Free") {
      this.freeSubs = "Free";
    } else if(name === "Medium") {
      this.mediumSubs = "Medium";
    } else {
      this.highSubs = "High";
    }
  } 

  private setImage(imageUrl: any) {
    this.userService.getImagePath(imageUrl).subscribe(data => {
      this.createImageFromBlob(data);
    }, error => {
      console.log(error);
    });
}

  private createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
        this.imageToShow = reader.result;
    }, false);

    if (image) {
        reader.readAsDataURL(image);
    }
  }

  OnSelectFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      const file: File = event.target.files[0];
      let token = localStorage.getItem('accessToken') as string;
      let tokenInfo = this.userService.getDecodedAccessToken(token);
      this.userService.uploadImage(tokenInfo.id, file).subscribe(
        res => {
          console.log(res);
        },
        err =>{
          console.log(err);
        }
      );

      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); 

      reader.onload = (event) => { 
        this.imageToShow = <string>reader.result;
      }
    }
  }

}
