import { Component, OnInit, Input } from '@angular/core';
import { FaceSnap } from '../models/face-snap.model';

@Component({
  selector: 'app-face-snap',
  templateUrl: './face-snap.component.html',
  styleUrls: ['./face-snap.component.scss']
})
export class FaceSnapComponent implements OnInit {
  @Input() faceSnap!: FaceSnap;

  title!: string;
  description!: string;
  createdDate!: Date;
  snaps!: number;
  imageUrl!: string;
  buttonText!: string;

  ngOnInit() {
    this.title = 'Archibald';
    this.description = 'This is my first post';
    this.createdDate = new Date();
    this.snaps = 6;
    this.imageUrl = 'https://plus.unsplash.com/premium_photo-1664474619075-644dd191935f?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1169&q=80';
    this.buttonText = "J'aime";
  }

  onLike() {
    if(this.buttonText === "J'aime") {
      this.faceSnap.snaps++;
      this.buttonText = "Je n'aime plus";
    } else {
      this.faceSnap.snaps--;
      this.buttonText = "J'aime";
    }
  }
}
