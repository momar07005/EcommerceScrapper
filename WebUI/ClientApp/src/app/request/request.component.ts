import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RepositoryService } from '../repository.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';


@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {

  requests: Observable<Request[]>;

  public displayedColumns = ['productId', 'date', 'numberOfReviews', 'status'];

  public dataSource = new MatTableDataSource<Request>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private repositoryService: RepositoryService, private router: Router) { }

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

}
