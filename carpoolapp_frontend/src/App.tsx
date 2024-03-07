import React, {useEffect} from 'react';
import logo from './logo.svg';
import './App.css';
import axios, {AxiosResponse} from "axios";
import {testDTO} from "./DTOs/test.model";

function App() {

  useEffect( () => {
    axios.get('http://localhost:5069/test')
        .then((response: AxiosResponse<testDTO[]>) => {
          console.log(response.data);
        })
  }, [])

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
