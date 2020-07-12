import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Tweet } from '../../../models/Tweet';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class TweetsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getTweets() {
    return this.http.get<Tweet[]>(this.baseUrl + 'tweets');
  }

}
