import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CacheService } from './cache-service';

@Component({
  standalone: true,
  selector: 'app-root',
  imports: [RouterOutlet, ReactiveFormsModule,HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'open-telemetry';

  constructor(private cacheService: CacheService) {}

  name = new FormControl('');
  surname = new FormControl('');

  save() {
       this.cacheService.addUser(this.name.value!, this.surname.value!).subscribe((response) => {
    });
  }
}
