import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BackupService } from 'src/services/backup.service';
import { UserService } from 'src/services/user.service';

@Component({
  selector: 'app-sidebar-admin',
  templateUrl: './sidebar-admin.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class SidebarAdminComponent implements OnInit {
  userDetails: any;
  imageToShow: any;

  constructor(private router: Router, private toastr: ToastrService, private userService: UserService,
    private backupService: BackupService) {
  }

  ngOnInit(): void {
    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.userService.getDecodedAccessToken(token);
    this.userService.getUserById(tokenInfo.id).subscribe(
      res => {
        this.userDetails = res
        this.setImage(this.userDetails.profileImage);
      },
      err =>{
        console.log(err);
      }
    );
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

  getBackup() {
    this.backupService.getSystemBackup().subscribe(
      res => {
        this.toastr.success('Backup was created successfully', 'System Backup');
      },
      err =>{
        console.log(err);
        this.toastr.error('There is an issue. Check the console.', 'Error');
      }
    );
  }

  redirectToUsers() {
    this.router.navigateByUrl("admin/dashboard");
  }

  redirectToManageBooks() {
    this.router.navigateByUrl("admin/manage-books");
  }


  showMessage() {
    this.toastr.info("This feature will be in the next release v.1.1");
  }

}
