import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SevillaComponent } from './sevilla/sevilla.component';
import { TweetsComponent } from './tweets/tweets.component';

@NgModule({
  declarations: [
    AppComponent,
    SevillaComponent,
    TweetsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
