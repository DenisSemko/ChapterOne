import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modal-subscription',
  templateUrl: './modal-subscription.component.html',
  styleUrls: ['../readerStyles.component.scss']
})
export class ModalSubscriptionComponent implements OnInit {

  constructor(private router: Router, public dialogRef: MatDialogRef<ModalSubscriptionComponent>) { }

  ngOnInit(): void {
  }

  seeDetails() {
    this.router.navigateByUrl("reader/subscription-details");
    this.closeDialog();
  }
  closeDialog(){
    this.dialogRef.close({event:'Cancel'});
  }

}
