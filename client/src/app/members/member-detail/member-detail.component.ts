import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  standalone: true, // because we use the ng-gallery inside this component --> ng-gallery is standalone
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss'],
  imports: [CommonModule, TabsModule, GalleryModule] // 2. part of standalone configuration
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
  images: GalleryItem[] = [];

  constructor(
    private membersService: MembersService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    this.membersService.getMember(username).subscribe({
      next: member => {
        this.member = member,
        this.getImages()
      }
    });
  }

  getImages() {
    if (!this.member) return;
    for (const photo of this.member?.photos) {
      this.images.push(new ImageItem({src: photo.url, thumb: photo.url}));
    }
  }
}
