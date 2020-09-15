import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RepositoryService } from '../repository.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { BulkRequest } from '../Request';
import { DialogBoxComponent } from '../dialog-box/dialog-box.component';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {

  requests: Observable<Request[]>;

  bulkRequest: BulkRequest;

  public displayedColumns = ['productId', 'date', 'numberOfReviews', 'status'];

  public dataSource = new MatTableDataSource<Request>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private repositoryService: RepositoryService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
     this.repositoryService.getAllRequests("Requests")
                        .subscribe(res => {
                                      this.dataSource.data = res as Request[];
                                   });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }


  openDialog(bulkRequest) {
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '350px',
      data: bulkRequest
    });

    dialogRef.afterClosed().subscribe(result => {
        
    });
  }

  //ToRequest(bulkRequest: BulkRequest): Request[] {
  //  let requests: Request[];
  //  bulkRequest.productIds.forEach(function (value) {
  //    requests.push(new Request(value, bulkRequest.numberOfReviews, bulkRequest.date));
  //  });
  //  return requests;
  //};

}
