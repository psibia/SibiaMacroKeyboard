#define sensorDelayRate 20
byte lastValue = 99;
void setup()
{
  Serial.begin(9600);
  Serial.setTimeout(50);
}
void loop()
{       
        byte value = key();
        if(value != lastValue)
        {
          lastValue = value;
          if(lastValue != 99)
            Serial.print(lastValue);
          delay(100);
        }
        //delay(sensorDelayRate);      
         
}
byte key()
{  
        int val = 1023 - analogRead(A0); 
        if      (val < 543 && val > 533) return 0; //538
        else if (val < 516 && val > 506) return 1; //511
        else if (val < 492 && val > 482) return 2; //487
        else if (val < 644 && val > 634) return 3; //639
        else if (val < 607 && val > 597) return 4; //602
        else if (val < 574 && val > 564) return 5; //569
        else if (val < 793 && val > 783) return 6; //788
        else if (val < 738 && val > 728) return 7; //733
        else if (val < 689 && val > 679) return 8; //684
        else if (val < 1028 && val > 1018) return 9; //1023
        else if (val < 938 && val > 928) return 10; //933
        else if (val < 862 && val > 852) return 11; //857
        else return 99;
}
