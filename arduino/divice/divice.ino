#define TRIGPIN_1 6
#define ECHOPIN_1 5
#define TRIGPIN_2 9
#define ECHOPIN_2 10

//取得超音波的距離
long getDistance(int trigpin, int echopin)
{
  digitalWrite(trigpin, LOW);
  delayMicroseconds(2); //    超音波 Trig  low 2us
  digitalWrite(trigpin, HIGH);
  delayMicroseconds(10); //    超音波 Trig Hi  發射信號 10us
  digitalWrite(trigpin, LOW);
  return pulseIn(echopin, HIGH) / 58; //  超音波 Echo 接收回傳信號計算距離
}

void setup()
{
  pinMode(TRIGPIN_1, OUTPUT);   //   Trig  為 輸出腳
  pinMode(ECHOPIN_1, INPUT);    //   Echo  為 輸入腳
  pinMode(TRIGPIN_2, OUTPUT);   //   Trig  為 輸出腳
  pinMode(ECHOPIN_2, INPUT);    //   Echo  為 輸入腳

  Serial.begin(9600); //開啟監控視窗
}
String out;
void loop()
{
    out = "";
    long leftNum = getDistance(TRIGPIN_1, ECHOPIN_1);
    if (leftNum > 0 && leftNum < 50) {
      out = "L/" + String(leftNum);
      Serial.println(out);
    }
    long rightNum = getDistance(TRIGPIN_2, ECHOPIN_2);
    if (rightNum > 0 && rightNum < 50) {
      out = "R/" + String(rightNum);
      Serial.println(out);
    }
    delay(600);
}
