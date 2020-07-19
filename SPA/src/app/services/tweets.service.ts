import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Tweet } from '../../../models/Tweet';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginationHeader } from 'models/PaginationHeader';
import { stringify } from 'querystring';

@Injectable({
  providedIn: 'root'
})

export class TweetsService {
  baseUrl = environment.apiUrl;
  totalRecords: string;
  tweetList: Tweet [];
  srchTerm: string;

  constructor(private http: HttpClient) { }


  getTweets(srchTerm: string, paginationHeader: PaginationHeader) {

    let params = new HttpParams();
    let headers: HttpHeaders = new HttpHeaders();

    this.srchTerm = srchTerm;

    params = params.append('srchItem', srchTerm);

    headers = headers.append('count', paginationHeader.pageSize.toString());

    if (paginationHeader.tweetId) {
      headers = headers.append('tweetDate', paginationHeader?.date);
      headers = headers.append('tweetId', paginationHeader?.tweetId.toString());
    }

    if (paginationHeader.tweetOperator) {
      headers = headers.append('operator', paginationHeader?.tweetOperator);
    }

    console.log('headers look like: ');
    console.log('headers object exists? ' + headers.keys());
    console.log('count : ' + headers.get('count'));
    console.log('date : ' + headers.get('tweetDate'));
    console.log('id : ' + headers.get('tweetId'));
    console.log('operator : ' + headers.get('operator'));

    return this.http.get<Tweet[]>(this.baseUrl + 'tweets', { observe: 'response', headers, params})
    .pipe(
      map(response => {
        this.tweetList = response.body;
        this.totalRecords = response.headers.get('ResultCount');
        console.log(`tweets service made it this far!`);
        return this.tweetList;
        })
      );
  }
}
