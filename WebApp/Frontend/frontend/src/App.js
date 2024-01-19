import logo from "./logo.svg";
import "./App.css";
import "./Components/Test";
import Joke from "./Components/Test";
import { Post } from "./Components/Post";
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          <Joke></Joke>
          <Post id_posta={1}></Post>
        </a>
      </header>
    </div>
  );
}

export default App;
