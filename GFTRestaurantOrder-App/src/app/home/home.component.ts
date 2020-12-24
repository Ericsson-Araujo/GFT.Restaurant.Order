import { Component, OnInit } from '@angular/core';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  test : String;
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder
    ) {
       //this.test = 'morning, 1, 2, 3';
       this.test = '';
      }

  validation() {
    this.registerForm = this.fb.group({
      // tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      // local: ['', Validators.required],
      // dataEvento: ['', Validators.required],
      // imagemURL: ['', Validators.required],
      // qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      // telefone: ['', Validators.required],
      // email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit() {
    this.validation();
  }

}
