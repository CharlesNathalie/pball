import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from 'src/app/app-material.module';
import { PageNotFoundComponent } from './components/pagenotfound/pagenotfound.component';
import { ShellComponent } from './components/shell/shell.component';

@NgModule({
  declarations: [
    PageNotFoundComponent,
    ShellComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ReactiveFormsModule,
    RouterModule,

    PageNotFoundComponent,
    ShellComponent,
  ]
})
export class SharedModule { }
