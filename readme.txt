1. เปิด command line cmd
2. ชี้ path ไปที่ Dockerfile อยู่
3. ใช้คำสั่งสร้าง image
	docker build -f Dockerfile -t plangrapi:v1 .
4. ใช้คำสั่งสร้าง container
	docker run --name PlanGRAPIContainer -p 8088:80 plangrapi:v1