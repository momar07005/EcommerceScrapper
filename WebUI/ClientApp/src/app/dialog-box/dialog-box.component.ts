import { Component, Inject, OnInit, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BulkRequest } from '../Request';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent implements OnInit {

  bulkRequest: BulkRequest;

  readonly chipsInputSeparatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    //@Optional() is used to prevent error if no data is passed
    @Optional() @Inject(MAT_DIALOG_DATA) public data: BulkRequest) {
    console.log(data);
    this.bulkRequest = { ...data };
  }

  doAction() {
    this.dialogRef.close({ event: 'Add', data: this.bulkRequest });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }

  ngOnInit(): void {
    this.bulkRequest.productIds = [];
  }

  addProductId(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    // Add our productId
    if ((value || '').trim()) {
      this.bulkRequest.productIds.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }
  }

  removeProductId(productId: string): void {
    const index = this.bulkRequest.productIds.indexOf(productId);

    if (index >= 0) {
      this.bulkRequest.productIds.splice(index, 1);
    }
  }

}
