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

  @ViewChild(MatPaginator) paginator: MatPaginator ;

  tweets: Tweet[];

  pageEvent: PageEvent;

  paginationHeader: PaginationHeader = {
    tweetId: null,
    date: null,
    tweetOperator: null,
    pageSize: null
  };

  // datasource: null;
  pageSize: number;
  length: string;

  srchTerm: string;

  pageIndex: number;
  previousPageIndex: number;

  constructor(private tweetsService: TweetsService) { }

  ngOnInit() {
    this.srchTerm = '';
    this.getTweets();
  }

// getTweets(srchTrm?: string, event?: PageEvent, isSrchChng?: boolean) {
getTweets(event?: PageEvent, enterKey: string = 'notEnter') {
  // if (this.srchTerm !== this.tweetsService.srchTerm && this.paginator) {
    console.log('my enter key is: ' + enterKey);
    if (enterKey === 'enter' && this.paginator) {
    this.paginator.firstPage(); }

    this.pageIndex = event?.pageIndex;
    this.previousPageIndex = event?.previousPageIndex;

  // tslint:disable-next-line:triple-equals
  // tslint:disable-next-line:triple-equals
  // this.srchTerm = (this.srchTerm == '') ? null : this.srchTerm;

  // tslint:disable-next-line:whitespace
    this.setPaginationHeader(event, enterKey);


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

setPaginationHeader(event?: PageEvent, enterKey?: string) {

  this.paginationHeader.tweetOperator = '';

  console.log('here your index and prev index: ' + event?.pageIndex + '  ' + event?.previousPageIndex);

  if (event) {
  console.log('welp, I knew I had an event');
  while (this.paginationHeader.tweetOperator === '') {
    console.log('entered the if event block');
    if (event?.pageIndex > event?.previousPageIndex && this.pageSize === event?.pageSize) {
      console.log('i thought it was a next page');
      this.paginationHeader.tweetId = this.tweets[this.tweets.length - 1].tweetId;
      this.paginationHeader.date = this.tweets?.[this.tweets.length - 1].time;
      this.paginationHeader.tweetOperator = '<';
      console.log('operator has now been set to: ' + this.paginationHeader.tweetOperator);
      break;
    }

    if (event?.pageIndex < event?.previousPageIndex && this.pageSize === event?.pageSize) {
      console.log('i thought it was a prev page');
      this.paginationHeader.tweetId = this.tweets[0].tweetId;
      this.paginationHeader.date = this.tweets?.[0].time;
      this.paginationHeader.tweetOperator = '>';
      break;
    }

    console.log('i thought  page sizes were: ' + this.pageSize + '  and  ' + event?.pageSize);
    if (this.pageSize !== event?.pageSize) {
      console.log('i thought it was a  page resize');
      this.paginationHeader.tweetId = this.tweets[0].tweetId;
      this.paginationHeader.date = this.tweets?.[0].time;
      this.paginationHeader.tweetOperator = 'size';
      this.pageSize = event?.pageSize;
      break;
  }

}

}

  console.log('i think my enter key right before enter is: ' + enterKey);
  if (enterKey === 'enter') {
  console.log('adding enter header now');
  this.paginationHeader.tweetOperator = 'search';
}


  console.log('yeah, there was no event and I know it');
  this.paginationHeader.pageSize = this.paginator ? this.paginator.pageSize : 50;
  // tslint:disable-next-line:whitespace

  // console.log(`ok, here's how i made your header:`);
  // console.log(`id: ` + this.paginationHeader.tweetId);
  // console.log(`date: ` + this.paginationHeader.date);
  // console.log(`operator: ` + this.paginationHeader.tweetOperator);
  // console.log(`pageSize: ` + this.paginationHeader.pageSize);
}

}
