import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SevillaComponent } from './sevilla/sevilla.component';
import { TweetsComponent } from './tweets/tweets.component';


const routes: Routes = [{ path: 'app-sevilla', component: SevillaComponent }
                      ,{ path: 'app-tweets', component: TweetsComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
