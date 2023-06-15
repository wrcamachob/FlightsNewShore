import { Component, OnInit, Input} from '@angular/core';
import { ComponentParent } from '../parents/component-parent';
import {JourneyService } from '../services/journey.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent extends ComponentParent implements OnInit {
  title = 'FlyNewShore';
  origin: string;
  destination: string;
  flightList: any = [];
  flightDetail: any = [];

  constructor(private journeyService: JourneyService,
    //private formBuilder: FormBuilder,
    private router: Router,
    //private location: Location
  ) {    
    super();    
    this.origin = "";
    this.destination = "";
  }

  ngOnInit() {    
    this.CleanData();
    //this.InitAutocomplete();
  }

  ValResult(data: any, error: any) {
    if (data != "" && data != null) {      
      this.alertOk(data, 'User');       
    } else if(error != "" && error != null){
      this.alertError(error, 'Error');
    } else {
      //this.errorHandled(data['message']);
    }
  } 

  GetJourney() {
    if (this.origin == this.destination){
      this.ValResult(null,"Fields cannot contain same value");
    }else if(this.origin.length!= 3){
      this.ValResult(null,"Origin must be 3 characters long");
    }else if(this.destination.length!= 3){
      this.ValResult(null,"Destination must be 3 characters long");
    }else{
      // const param = {
      //   origin: this.origin.toUpperCase(),
      //   destination: this.destination.toLocaleUpperCase()
      // }
      this.origin.toUpperCase();
      this.destination.toLocaleUpperCase()
      this.journeyService.Get(this.origin, this.destination).subscribe(
        data => {
          if (data != "") {
            this.flightList = data;
            this.fliDetail();          
            //console.log(this.flightList)          
          }
          else{
            this.CleanData();
            this.ValResult(null,"No Information");
          }
        });
    }    
  }

  fliDetail() {    
    for(let i=0;i<this.flightList.length; i++){   
      this.flightDetail = this.flightList[i].flights
    }
    //this.flightDetail = this.flightList.filter((item: any) => item.flights === 'flights');
    // this.flightDetail = this.flightList.map((element: "") => {
    //   return { nickName: element.name };
    // });    
  }

  CleanData() {
    debugger;
    this.origin = "";
    this.destination = "";
    this.flightList = [];
    this.flightDetail = []; 
  }
}
