import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { Tweet } from 'models/Tweet';
import { PageEvent, MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.scss']
})
export class TweetsComponent implements OnInit {

  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator ;

  tweets: Tweet[];


  pageEvent: PageEvent;
  datasource: null;
  pageSize: number;
  length: string;

  srchTerm: string;

  constructor(private tweetsService: TweetsService) { }

  ngOnInit() {
    // this.getTweets();
    // this.pageSize = 50;
  }


// getTweets(srchTrm?: string, event?: PageEvent, isSrchChng?: boolean) {
getTweets() {
  // const itemsCount = event.pageSize ? event.pageSize.toString() : null;
  if (this.srchTerm !== this.tweetsService.srchTerm) {
    console.log('yep, changed srch term. changing index to 0!');
    this.paginator.firstPage(); }

  this.tweetsService.getTweets(this.srchTerm, this.paginator.pageSize)
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

}
