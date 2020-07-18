import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { Tweet } from 'models/Tweet';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { PaginationHeader } from 'models/PaginationHeader';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.scss']
})
export class TweetsComponent implements OnInit {

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator ;

  tweets: Tweet[];

  pageEvent: PageEvent;
  paginationHeader: PaginationHeader;
  // datasource: null;
  // pageSize: number;
  length: string;

  srchTerm: string;

  constructor(private tweetsService: TweetsService) { }

  ngOnInit() {
    this.srchTerm = '';
    this.getTweets();
  }

// getTweets(srchTrm?: string, event?: PageEvent, isSrchChng?: boolean) {
getTweets() {
  if (this.srchTerm !== this.tweetsService.srchTerm && this.paginator) {
    this.paginator.firstPage(); }

  // tslint:disable-next-line:triple-equals
  // tslint:disable-next-line:triple-equals
  this.srchTerm = (this.srchTerm == '') ? null : this.srchTerm;

  this.setPaginationHeader();

  this.tweetsService.getTweets(this.srchTerm, this.paginationHeader)
  .subscribe(tweets => {
    this.tweets = tweets;
  }, error => {
    console.log(error);
  }, () => {
    this.length = this.tweetsService.totalRecords;

    console.log('here your value: ' + this.srchTerm);
    console.log('here your length: ' + this.length);
  });
}

setPaginationHeader() {
  this.paginationHeader.pageSize = this.paginator ? this.paginator.pageSize : 50;
  // tslint:disable-next-line:whitespace
  this.paginationHeader.firstDate = this.tweets?.[0].time;
  // tslint:disable-next-line:whitespace
  this.paginationHeader.lastDate = this.tweets?.[this.tweets.length - 1].time;
}

}
