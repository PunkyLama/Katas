import { Component, OnInit} from '@angular/core';
import { FaceSnap } from './models/face-snap.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  faceSnaps!: FaceSnap[];
  ngOnInit(){
    this.faceSnaps = [{
          title:'Archibald',
          description:'This is my first post',
          createdDate:new Date(),
          snaps:46,
        imageUrl:'https://plus.unsplash.com/premium_photo-1664474619075-644dd191935f?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1169&q=80',
       location:'Paris'
      },{
      title:'Phoque',
      description:'This is a Fock',
      createdDate:new Date(),
      snaps:150,
      imageUrl:'https://cdn.pixabay.com/photo/2023/09/04/08/54/sea-lion-8232312_1280.jpg'
    },{
      title:'Avion',
      description:'NIOOOOOOOOOOOOOOONNNNNNNNNNNNNNNNNNNNNN',
      createdDate:new Date(),
      snaps:6,
      imageUrl:'https://cdn.pixabay.com/photo/2023/07/14/10/30/de-havilland-tiger-moth-8126721_1280.jpg',
      location:'Dans les airs'
    }];
  }
}
