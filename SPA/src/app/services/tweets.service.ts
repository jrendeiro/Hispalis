import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Tweet } from '../../../models/Tweet';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { PaginationHeader } from 'models/PaginationHeader';

@Injectable({
  providedIn: 'root'
})

export class TweetsService {
  baseUrl = environment.apiUrl;
  totalRecords: string;
  tweetList: Tweet [];
  srchTerm: string;

  constructor(private http: HttpClient) { }


  getTweets(srchItem: string, paginationHeader: PaginationHeader) {

    let params = new HttpParams();
    let headers: HttpHeaders = new HttpHeaders();

    this.srchTerm = srchItem;

    params = params.append('srchItem', srchItem);
    headers = headers.append('count', paginationHeader.pageSize.toString());

    return this.http.get<Tweet[]>(this.baseUrl + 'tweets', { observe: 'response', headers, params})
      .pipe(
        map(response => {
          this.tweetList = response.body;
          this.totalRecords = response.headers.get('ResultCount');
          return this.tweetList;
        })
      );
  }
}
