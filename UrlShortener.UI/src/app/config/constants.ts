import { Injectable } from '@angular/core';

@Injectable({providedIn: 'root'})
export class Constants {
    
    public API_ENDPOINT: string = 'https://localhost:44377/'  
    public EXPIRY_DATE: string = '2023-10-22T00:00:00.000'
    public UI_MESSAGE: string ='Result will appear here!'
}