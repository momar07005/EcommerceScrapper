import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RepositoryService } from '../repository.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Review } from '../Review';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.css']
})
export class ReviewComponent implements OnInit {

  reviews: Observable<Review[]>;

  public displayedColumns = ['AINS', 'ReviewId', 'Title', 'Content', 'Rate', 'Date' ];

  public dataSource = new MatTableDataSource<Review>();

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private repositoryService: RepositoryService, private router: Router) { }

  ngOnInit(): void {
    this.dataSource.filterPredicate = function (data, filter: string): boolean {
      return data.content.toLowerCase().includes(filter);
    };

    this.loadData();
  }

  loadData() {
    this.repositoryService.getAllReviews("Reviews")
      .subscribe(response => {
        this.dataSource.data = response
      });
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }
}

