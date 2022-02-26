package com.example.vrchaep_application;

import static android.content.ContentValues.TAG;

import androidx.appcompat.app.AppCompatActivity;

import android.bluetooth.BluetoothSocket;
import android.os.Bundle;
import android.util.Log;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

public class Client_Activity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_client);
    }

    class  ConnectedThread extends Thread {

        private final BluetoothSocket socket;
        private final InputStream inputStream ;
        private final OutputStream outputStream;

        ConnectedThread(BluetoothSocket socket){
            this.socket = socket;
            InputStream tmpI = null;
            OutputStream tmpO = null;

            try {
                tmpI = socket.getInputStream();
                tmpO = socket.getOutputStream();
            }catch (IOException e) {
                Log.e(TAG, "Could not get streams", e);
            }

            this.inputStream = tmpI;
            this.outputStream = tmpO;
        }


        public BluetoothSocket getSocket() {
            return socket;
        }

        public void run() {
            byte[] buffer = new byte[1024];
            int bytes;
            while (socket.isConnected()) {
                try{
                    bytes = inputStream.read(buffer);
                    String message = new String(buffer, 0, bytes);
                    Object onMessageListener;
                    //send(gameObject, message);
                }catch (IOException e) {
                    //send(serverObject, "socket.error.COULD_NOT_READ");
                    cancel();
                    break;
                }
            }
        }

        void write(byte[] buffer){
            try{
                outputStream.write(buffer);
            }catch (IOException e){
                cancel();
                Log.e(TAG, "Exception during write", e);
            }
        }

        void cancel(){
            try {
                inputStream.close();
                outputStream.close();
                socket.close();
            }catch (IOException e){
                Log.e(TAG, "Could not close", e);
            }
        }
    }

}