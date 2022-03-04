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
  url = '';

  constructor(private router: Router, private toastr: ToastrService, private userService: UserService,
    private backupService: BackupService) {
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
        console.log(this.userDetails);
      },
      err =>{
        console.log(err);
      }
    );
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

}
