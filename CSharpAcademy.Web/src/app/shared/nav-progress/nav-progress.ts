import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-nav-progress',
  standalone: false,
  templateUrl: './nav-progress.html',
  styleUrl: './nav-progress.css',
})
export class NavProgress implements OnInit, OnDestroy {
  visivel = false;
  completo = false;
  private sub = new Subscription();

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.sub.add(
      this.router.events.subscribe(ev => {
        if (ev instanceof NavigationStart) {
          this.completo = false;
          this.visivel = true;
        } else if (
          ev instanceof NavigationEnd ||
          ev instanceof NavigationCancel ||
          ev instanceof NavigationError
        ) {
          this.completo = true;
          setTimeout(() => { this.visivel = false; }, 300);
        }
      })
    );
  }

  ngOnDestroy(): void { this.sub.unsubscribe(); }
}
