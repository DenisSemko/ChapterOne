import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BackupService {

  constructor(private http: HttpClient) { }

  tokenHeader = new HttpHeaders({'Authorization' : 'Bearer ' + localStorage.getItem('accessToken')});

  getSystemBackup() {
    return this.http.get(environment.baseURI + 'Backup', {headers: this.tokenHeader});
  }
}
