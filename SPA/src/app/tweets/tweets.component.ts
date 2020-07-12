import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { Tweet } from 'models/Tweet';

@Component({
  selector: 'app-tweets',
  templateUrl: './tweets.component.html',
  styleUrls: ['./tweets.component.scss']
})
export class TweetsComponent implements OnInit {

  tweets: Tweet[];

  constructor(private tweetsService: TweetsService) { }

  ngOnInit() {
    this.getTweets();
  }


getTweets() {
  this.tweetsService.getTweets()
    .subscribe(tweets => {
      this.tweets = tweets;
    }, error => {
      console.log(error);
    }, () => {
      console.log(`tweet 7: ` + this.tweets[6].location);
    });
}

}
