import { Component } from '@angular/core';
import { Constants } from 'src/app/config/constants';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  inputUrl = '';
  shortenedUrl = '';
  eventsSubject: Subject<void> = new Subject<void>();

  constructor(
    private http: HttpClient,
    private constants: Constants
  ) {
    this.shortenedUrl = constants.UI_MESSAGE;
  }

  submitURL(value: string) {
    let endpoint = `${this.constants.API_ENDPOINT}api/UrlShortener/PostUrl`;
    return this.http
      .post<ShortenedUrl>(
        endpoint,
        {
          longUrl: this.inputUrl,
          expiration: this.constants.EXPIRY_DATE,
        },
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .subscribe((result) => {
        this.shortenedUrl = result.shortUrl;
        if (result != null){
          this.eventsSubject.next();
        }         
      });
  }
}

export interface ShortenedUrl {
  shortUrl: string;
}