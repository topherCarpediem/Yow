import React from 'react';
import logo from './logo.svg';
import './App.css';
import { decode } from 'base64-arraybuffer';
import { HubConnectionBuilder, HubConnection, LogLevel } from "@aspnet/signalr";


class App extends React.Component<{}> {

  connection: HubConnection;

  constructor(props: any) {
    super(props);

    this.connection = new HubConnectionBuilder()
        .configureLogging(LogLevel.Debug)
        .withUrl("http://localhost:5100/chatHub")
        .build();

    
    this.connection.start()
    .then(() => {

    })
    .catch(err => {
      console.log(err)
    })
  }

  encryptDecrypt = async () => {

    const recieverEcdhJwk: JsonWebKey = {
      kty: "EC",
      crv: "P-256",
      x: "VP2sqUIh7wpzHQpfS98bWSUNETbv8-lYyyAjbIyOoWQ",
      y: "zegnwwPl4VqOuFlqs2BrRZ02nt_EM1d5LFyWY7172Us",
      d: "YIbKJhHuRDuxarm1qt9nGqdINDKn8mqKdOoIYmGrYwk",
      ext: true
    }

    const senderECDHJwk: JsonWebKey = {
      kty: "EC",
      crv: "P-256",
      x: "cboWOjo9OmP6wJG0dZtpEDQP0I7NZQlxIvZ_qtI8Gn4",
      y: "7T4S85mVc7OZjApOApjZsOS6ocb08nBZQk2Bd-f_YsI",
      ext: true,
    }


    const recieverEcdhOpts: EcKeyImportParams = { name: "ECDH", namedCurve: "P-256" }

    const senderEcdhOpts: EcKeyImportParams = {
      name: "ECDH",
      namedCurve: "P-256",
    }

    const encryptedMessage : ArrayBuffer = decode("0mDDlK7bWlT9V9DcGidgNg==");
    const salt : ArrayBuffer = decode("EWw5awTUEBH7g4KaFI/7sA==")
    const signature : ArrayBuffer = decode("V/f8zhk5TbJ4OqyvivliXYrMmCK5ouD00g0WSHi9Oq/w5rWRYGvOwhfiHfJ9s7IwzJLG3aagppL/b6zYUkDyPw==");

    const aesDeriveKeyOpts: AesDerivedKeyParams = { name: "AES-CBC", length: 256 }

    const recieverEcdsa = await crypto.subtle.importKey(
      "jwk", senderECDHJwk,
      {   //these are the algorithm options
          name: "ECDSA",
          namedCurve: "P-256" 
      }, true,  ["verify"])


    const recieverPrivateKey: CryptoKey = await crypto.subtle.importKey("jwk", recieverEcdhJwk, recieverEcdhOpts, false, ["deriveKey"])
    const senderPublicKey: CryptoKey = await crypto.subtle.importKey("jwk", senderECDHJwk, senderEcdhOpts, false, [])

    const ecdhKeyDeriveOpts: EcdhKeyDeriveParams = { name: "ECDH", public: senderPublicKey }

    const derivedSharedSecret: CryptoKey = await crypto.subtle.deriveKey(ecdhKeyDeriveOpts, recieverPrivateKey, aesDeriveKeyOpts, true, ["encrypt", "decrypt"])
    const sharedSecretDeriveBits: ArrayBuffer = await crypto.subtle.exportKey("raw", derivedSharedSecret);

    const digestSharedSecret: ArrayBuffer = await crypto.subtle.digest({ name: "SHA-256", }, sharedSecretDeriveBits)
    const secretKey: CryptoKey = await window.crypto.subtle.importKey("raw", digestSharedSecret, aesDeriveKeyOpts, true, ["encrypt", "decrypt"])

    const decrypted: ArrayBuffer = await window.crypto.subtle.decrypt({ name: 'AES-CBC', length: 256, iv: salt }, secretKey, encryptedMessage)


    const result = await crypto.subtle.verify({
        name: "ECDSA",
        hash: { name: "SHA-256"},
      },
      recieverEcdsa,
      signature,
      encryptedMessage 
    );

    console.log(result)

    // console.log(result)
    return new TextDecoder().decode(decrypted);


  }

  componentDidMount() {

    this.encryptDecrypt()
    .then(decypted => {
      console.log(decypted)
    })
    .catch(e => {
      console.log(e)
    })


  }

  render() {
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
}

export default App;
