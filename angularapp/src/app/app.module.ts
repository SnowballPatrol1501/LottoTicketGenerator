import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { LottoTicketModule } from './lotto-ticket/lotto-ticket.module';
import { AppRouting } from './app.routes';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    AppRouting,
    BrowserModule, 
    HttpClientModule,
    LottoTicketModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
