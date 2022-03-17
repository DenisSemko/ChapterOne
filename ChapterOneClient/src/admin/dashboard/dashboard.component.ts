import { Component, OnInit, ViewChild} from '@angular/core';
import { User } from 'src/models/user';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from 'src/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { UsersActionsComponent } from '../users-actions/users-actions.component';
import { UsersActionDeleteComponent } from '../users-action-delete/users-action-delete.component';
import { UsersActionAddAdminComponent } from '../users-action-add-admin/users-action-add-admin.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['../adminStyles.component.scss']
})
export class DashboardComponent implements OnInit {
  userDetails: any;
  users: User[]= [];
  displayedColumns: string[] = ['Name', 'Surname', 'BirthDate', 'Email', 'Username', 'Actions'];

  dataSource = new MatTableDataSource<User>(this.users);

  @ViewChild(MatPaginator, { static: true })
  paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true })
  sort!: MatSort;

  constructor(public service: UserService, private toastr: ToastrService, public dialog: MatDialog) { }

  ngOnInit(): void {

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort=this.sort;

    let token = localStorage.getItem('accessToken') as string;
    let tokenInfo = this.service.getDecodedAccessToken(token);
    
    this.service.getAllUsers().subscribe(
      result => {
        this.userDetails = (result as User[]);
        for(var i = 0; i < this.userDetails.length; i++) {
          if(this.userDetails[i].id === tokenInfo.id) {
            const index: any = (result as User[]).indexOf((result as User[])[i]);
            this.userDetails.splice(index, 1);
          }
        }
        this.dataSource.data = this.userDetails;
      },
      error =>{
        console.log(error);
      }
    );
  }

  openUpdateDialog(action: any,obj: { action: any; }) {
    obj.action = action;
    const dialogRef = this.dialog.open(UsersActionsComponent, {
      width: '30%',
      data:obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === true)
        this.service.updateUserByAdmin(result.data);
    });
  }

  openDeleteDialog(id: string) {
    const dialogRef = this.dialog.open(UsersActionDeleteComponent);
    dialogRef.afterClosed().subscribe(result => {
      if(result === true)
        this.service.deleteUser(id).subscribe(
          result => {
            this.toastr.success('User has been deleted!');
            window.location.reload();
          }, error => {
            console.log(error);
          }
        )
    });
  }

  openAddAdminDialog() {
    const dialogRef = this.dialog.open(UsersActionAddAdminComponent, {
      width: '80%',
      height: '80%',
      panelClass: 'custom-modalbox',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result === true)
        this.toastr.success('Admin has been added!');
        window.location.reload();
      });
  }
}
